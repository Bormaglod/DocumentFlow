<?php
// Copyright © 2018-2020 Тепляшин Сергей Васильевич. Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0

namespace App\Controller;

use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;
use App\Connection\PostgresConnection;
use App\Exception\AccessException;
use App\Exception\VersionException;
use App\Exception\NotImplementedException;
use App\Exception\BadParameterException;

class DatasetController extends DatabaseController {
   public function get(Request $request, Response $response, $args) {
      try {
         $this->checkAccess($request);

         $connect = new PostgresConnection();
         $data = $connect->execute($this->getSQLQuery($request));

         return $response->withJson($data);
      } catch (\PDOException $e) {
         return $response->withJson(['error_code' => $e->getCode(), 'message' => $e->getMessage(), 'extended' => $this->getExtendErrors($e)], self::HTTP_INTERNAL_SERVER_ERROR);
      } catch (AccessException | VersionException | BadParameterException $e) {
         return $response->withJson($e->getMessageData(), $e->getHttpCode());
      }
   }

   function getById(Request $request, Response $response, $args) {
      try {
         $this->checkAccess($request);
   
         $connect = new PostgresConnection();
         $data = $connect->execute($this->getSQLQueryById($request), $args);
   
         if ($data['total_rows'] == 0) {
            return $response->withJson(['error_code' => self::OBJECT_NOT_EXISTS, 'message' => 'Объект с таким id не существует.'], self::HTTP_NOT_FOUND);
         }
   
         return $response->withJson($data);
      } catch (\PDOException $e) {
         return $response->withJson(['error_code' => $e->getCode(), 'message' => $e->getMessage(), 'extended' => $this->getExtendErrors($e)], self::HTTP_INTERNAL_SERVER_ERROR);
      } catch (AccessException | VersionException $e) {
         return $response->withJson($e->getMessageData(), $e->getHttpCode());
      }
   }

   function post(Request $request, Response $response, $args) {
      try {
         $this->checkAccess($request);
   
         $connect = new PostgresConnection();
         $entity = $this->getEntityName($request);
         $id = $connect->insert($entity, $request->getParsedBody());
   
         return $response
            ->withStatus(self::HTTP_CREATED)
            ->withHeader('Location', "/{$entity}/{$id}");
      } catch (\PDOException $e) {
         return $response->withJson(['error_code' => $e->getCode(), 'message' => $e->getMessage(), 'extended' => $this->getExtendErrors($e)], self::HTTP_INTERNAL_SERVER_ERROR);
      } catch (AccessException | VersionException $e) {
         return $response->withJson($e->getMessageData(), $e->getHttpCode());
      }
   }

   function put(Request $request, Response $response, $args) {
      try {
         $this->checkAccess($request);
   
         $connect = new PostgresConnection();
         $connect->updateAll($this->getEntityName($request), $args['id'], $this->getFields($request), $request->getParsedBody());
   
         return $response->withStatus(self::HTTP_NO_CONTENT);
      } catch (\PDOException $e) {
         return $response->withJson(['error_code' => $e->getCode(), 'message' => $e->getMessage(), 'extended' => $this->getExtendErrors($e)], self::HTTP_INTERNAL_SERVER_ERROR);
      } catch (AccessException | VersionException $e) {
         return $response->withJson($e->getMessageData(), $e->getHttpCode());
      }
   }

   function patch(Request $request, Response $response, $args) {
      try {
         $this->checkAccess($request);
   
         $connect = new PostgresConnection();
         $connect->update($this->getEntityName($request), $args['id'], $request->getParsedBody());
   
         return getFilteredQuery($request, $response, $this->getSQLQueryById($request), ['id' => $id]);
      } catch (\PDOException $e) {
         return $response->withJson(['error_code' => $e->getCode(), 'message' => $e->getMessage(), 'extended' => $this->getExtendErrors($e)], self::HTTP_INTERNAL_SERVER_ERROR);
      } catch (AccessException | VersionException $e) {
         return $response->withJson($e->getMessageData(), $e->getHttpCode());
      }
   }

   function changeState(Request $request, Response $response, $args) {
      try {
         $this->checkAccess($request);
   
         $connect = new PostgresConnection();
         $connect->update($this->getEntityName($request), $args['id'], $request->getParams());
   
         return getFilteredQuery($request, $response, $this->getSQLQueryById($request), ['id' => $args['id']]);
      } catch (\PDOException $e) {
         return $response->withJson(['error_code' => $e->getCode(), 'message' => $e->getMessage(), 'extended' => $this->getExtendErrors($e)], self::HTTP_INTERNAL_SERVER_ERROR);
      } catch (AccessException | VersionException $e) {
         return $response->withJson($e->getMessageData(), $e->getHttpCode());
      }
   }

   function delete(Request $request, Response $response, $args) {
      try {
         $this->checkAccess($request);
   
         $connect = new PostgresConnection();
         $connect->delete($this->getEntityName($request), $args['id']);
   
         return $response->withStatus(self::HTTP_NO_CONTENT);
      } catch (\PDOException $e) {
         getExtendErrors($e);
         return $response->withJson(['error_code' => $e->getCode(), 'message' => $e->getMessage(), 'extended' => $ths->getExtendErrors($e)], self::HTTP_INTERNAL_SERVER_ERROR);
      } catch (AccessException | VersionException $e) {
         return $response->withJson($e->getMessageData(), $e->getHttpCode());
      }
   }

   protected function getEntityName(Request $request) {
      throw new NotImplementedException('Функция getEntityName() не реализована.');
   }

   protected function getSQLQuery(Request $request) {
      throw new NotImplementedException('Функция getSQLQuery() не реализована.');
   }

   protected function getSQLQueryById(Request $request) {
      throw new NotImplementedException('Функция getSQLQueryById() не реализована.');
   }

   protected function getFields(Request $request) {
      throw new NotImplementedException('Функция getFields() не реализована.');
   }
}

?>