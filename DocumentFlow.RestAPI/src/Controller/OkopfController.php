<?php
// Copyright © 2018-2020 Тепляшин Сергей Васильевич. Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0

namespace App\Controller;

use Psr\Http\Message\ServerRequestInterface as Request;

class OkopfController extends DatasetController {
   protected function getEntityName(Request $request) {
      return 'okopf';
   }

   protected function getSQLQuery(Request $request) {
      return 'select o.id, o.status_id, s.note as status_name, o.code, o.name from okopf o join status s on (s.id = o.status_id)';
   }

   protected function getSQLQueryById(Request $request) {
      return 'select o.id, o.status_id, s.note as status_name, o.code, o.name from okopf o join status s on (s.id = o.status_id) where o.id = :id';
   }

   protected function getFields(Request $request) {
      return ['code', 'name'];
   }
}

?>