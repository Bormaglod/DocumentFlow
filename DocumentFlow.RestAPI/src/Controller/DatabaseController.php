<?php
// Copyright © 2018-2020 Тепляшин Сергей Васильевич. Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0

namespace App\Controller;

use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;
use Firebase\JWT\JWT;
use App\Exception\AccessException;
use App\Exception\VersionException;
use App\Connection\PostgresConnection;
use Firebase\JWT\ExpiredException;
use Firebase\JWT\SignatureInvalidException;

class DatabaseController {
   const HTTP_CREATED               = 201;
   const HTTP_NO_CONTENT            = 204;
   const HTTP_BAD_REQUEST           = 400;
   const HTTP_UNAUTHORIZED          = 401;
   const HTTP_NOT_FOUND             = 404;
   const HTTP_INTERNAL_SERVER_ERROR = 500;

   const TOKEN_LIFETIME_EXPIRED   = 600000;
   const AUTHORIZATION_REQUIRED   = 600001;
   const SIGN_VERIFICATION_FAILED = 600002;
   const INVALID_TOKEN            = 600003;
   const API_VERSION_REQUIRED     = 601000;
   const OBJECT_NOT_EXISTS        = 602000;
   const USER_NOT_REGISTERED      = 603000;
   const BAD_PARAMETER            = 604000;

   const SECRET_KEY = 'W09mjyOHBs';

   public function login(Request $request, Response $response, $args) {
      $version = $this->getVersion($request);
      if ($version == 0) {
         return $response->withJson(['error_code' => self::API_VERSION_REQUIRED, 'message' => 'Необходимо указать версию API.'], self::HTTP_BAD_REQUEST);
      }

      $params = $request->getParsedBody();

      try {
         $connect = (new PostgresConnection(["user" => 'guest', "password" => 'guest']))->get();
         
         $query = $connect->prepare('select id, password from user_alias where name = :name');
         $query->bindValue(':name', $params['username']);
         $query->execute();
         $user = $query->fetch(\PDO::FETCH_OBJ);
   
         if ($user and $user->password == $params['password']) {
   
            $connect = (new PostgresConnection())->get();
         
            $query = $connect->prepare('select login()');
            $query->execute();
    
            $jwt = $this->createToken($user->id, $params['username']);
   
            $query = $connect->prepare("update user_alias set token = :token where id = :user_id");
            $query->bindValue(':token', $jwt);
            $query->bindValue(':user_id', $user->id);
            $query->execute();
   
            return $response->withJson(['token' => $jwt]);
         } else {
            return $response->withJson(['error_code' => self::USER_NOT_REGISTERED, 'message' => 'Пользователь ' . $params['username'] . ' не зарегестрирован.'], self::HTTP_BAD_REQUEST);
         }
      } catch (PDOException $e) {
         return $response->withJson(['error_code' => $e->getCode(), 'message' => $e->getMessage()], self::HTTP_INTERNAL_SERVER_ERROR);
      }
   }

   protected function getVersion(Request $request) {
      $accept = strtolower(str_replace(' ', '', $request->getHeaderLine('Accept')));
   
      $accept_list = explode(',', $accept);
      foreach ($accept_list as $item) {
         $item_list = explode(';', $item);
         if ($item_list[0] === 'application/vnd.workflow-enterprise.api+json') {
            $version = explode('=', $item_list[1]);
            return $version[1];
         }
      }
   
      return 0;
   }

   protected function createToken($id, $user_name)
   {
      $expire = strtotime('+1 day');

      $token = [
         'exp' => $expire,             // время жизни токена
         'user' => [$id, $user_name]   // идентификатор и имя пользователя
      ];

      return JWT::encode($token, self::SECRET_KEY);
   }

   protected function checkToken(Request $request) {
      if ($request->hasHeader('Authorization')) {
         $auth = $request->getHeaderLine('Authorization');
         list($jwt) = sscanf($auth, 'Bearer %s');
         try {
            $token = JWT::decode($jwt, self::SECRET_KEY, ['HS256']);
         }
         catch (ExpiredException $e) {
            return array('error_code' => self::TOKEN_LIFETIME_EXPIRED, 'message' => 'Истекло время жизни токена.');
         }
         catch (SignatureInvalidException $e) {
            return array('error_code' => self::SIGN_VERIFICATION_FAILED, 'message' => 'Invalid token. Signature verification failed.');
         }
         catch (\UnexpectedValueException $e) {
            return array('error_code' => self::INVALID_TOKEN, 'message' => 'Invalid token.');
         }
      } else {
         return array('error_code' => self::AUTHORIZATION_REQUIRED, 'message' => 'Для выполнения запроса необходима авторизация пользователя.');
      }
   }

   protected function checkAccess(Request $request) {
      $version = $this->getVersion($request);
      if ($version == 0) 
         throw new VersionException('Необходимо указать версию API.', self::API_VERSION_REQUIRED);
    
      $access = $this->checkToken($request);
      if ($access) {
         throw new AccessException($access['message'], $access['error_code']);
      }
   
      return true;
   }

   protected function getExtendErrors($exception) {
      if (is_numeric($exception->getCode())) {
         $re = '/SQLSTATE\[(.+?)\]:\s+(.+?)\s+ERROR:\s+(.+)\s+DETAIL:\s+(.+)/';
         $name = 'detail';
      } else {
         $re = '/SQLSTATE\[(.+?)\]:\s+(.+?)\s+ERROR:\s+(.+)\s+CONTEXT:\s+(.+)/';
         $name = 'context';
      }

      preg_match_all($re, $exception->getMessage(), $matches, PREG_SET_ORDER, 0);
   
      if (count($matches) > 0) {
         return ['sqlstate' => $matches[0][1], 'name' => $matches[0][2], 'message' => $matches[0][3], $name => $matches[0][4]];
      }
      else
         return [];
   }
}

?>