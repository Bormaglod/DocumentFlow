<?php
// Copyright © 2018-2020 Тепляшин Сергей Васильевич. Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0

namespace App\Connection;

use PDO;

class PostgresConnection {
    private $connect = null;

    private $host = 'localhost';
    private $db   = 'document_flow';
    private $user = 'def_user';
    private $password = 'CeFdrHlflo';
    private $port = 5432;

    public function __construct(array $user_param = []) {
        $this->host = $user_param['host'] ?? $this->host;
        $this->db = $user_param['db'] ?? $this->db;
        $this->user = $user_param['user'] ?? $this->user;
        $this->password = $user_param['password'] ?? $this->password;
        $this->port = $user_param['port'] ?? $this->port;
    }

    protected function connectionPDO() {
        $dsn = "pgsql:host=$this->host;dbname=$this->db;port=$this->port";
        $opt = array(
            PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
            PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
            PDO::ATTR_EMULATE_PREPARES   => false);
        $this->connect = new PDO($dsn, $this->user, $this->password, $opt);
    }

    public function get(array $user_param = []) {
        $old_user = $this->user;
        $old_password = $this->password;

        $this->user = $user_param['user'] ?? $this->user;
        $this->password = $user_param['password'] ?? $this->password;
        
        if ($old_password != $this->password || $old_user !== $this->user) {
            $this->connect = null;
        }

        if ($this->connect) {
            return $this->connect;
        } else {
            $this->connectionPDO();
            return $this->connect;
        }
    }

    public function execute($sql, $args = null) {
        $connect = $this->get();
        $query = $connect->prepare($sql);
        if (isset($args)) {
            foreach ($args as $key => $value) {
                $query->bindValue(":{$key}", $value);
            }
        }

        $query->execute();

        $total_rows = $query->rowCount();
        $rows = $query->fetchAll();

        $data = [ 'total_rows' => $total_rows, 'rows' => $rows ];

        return $data;
    }

    public function insert($entity, $args = null) {
        $connect = $this->get();

        if ($args and count($args) > 0) {
            $fields = array();
            $values = array();
            foreach ($args as $key => $value) {
                $fields[] = $key;
                $values[] = ':'.$key;
            }

            $str_fields = implode(',', $fields);
            $str_values = implode(',', $values);
            $query = $connect->prepare("insert into {$entity} ({$str_fields}) values ({$str_values}) returning id");

            foreach ($args as $key => $value) {
                $query->bindValue(':'.$key, $value);
            }
        } else {
            $query = $connect->prepare("insert into {$entity} default values returning id");
        }

        $query->execute();

        $id = $query->fetch(PDO::FETCH_NUM);
        return $id[0];
    }

    public function update($entity, $id, $params) {
        $set = array();
        foreach ($params as $key => $value) {
            $set[] = $key.'=:'.$key;
        }

        $str_set = implode(',', $set);

        $connect = $this->get();
        $query = $connect->prepare("update {$entity} set {$str_set} where id = :id");

        foreach ($params as $key => $value) {
            $query->bindValue(':'.$key, $value);
        }

        $query->bindValue(':id', $id);

        $query->execute();
    }

    public function updateAll($entity, $id, $fields, $params) {
        $set = array();
        foreach ($fields as $value) {
            $set[] = $value.'=:'.$value;
        }

        $str_set = implode(',', $set);

        $connect = $this->get();
        $query = $connect->prepare("update {$entity} set {$str_set} where id = :id");

        foreach ($params as $key => $value) {
            $query->bindValue(':'.$key, $value);
        }

        $query->bindValue(':id', $id);

        $query->execute();
    }

    public function delete($entity, $id) {
        $connect = $this->get();
        $query = $connect->prepare("delete from {$entity} where id = :id");
        $query->bindValue(':id', $id);
        $query->execute();
    }
}

?>