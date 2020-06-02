<?php
// Copyright © 2018-2020 Тепляшин Сергей Васильевич. Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0

namespace App\Controller;

use Psr\Http\Message\ServerRequestInterface as Request;

class MeasurementController extends DatasetController {
   protected function getEntityName(Request $request) {
      return 'measurement';
   }

   protected function getSQLQuery(Request $request) {
      return 'select m.id, m.status_id, s.note as status_name, m.code, m.name, m.abbreviation from measurement m join status s on (s.id = m.status_id)';
   }

   protected function getSQLQueryById(Request $request) {
      return 'select m.id, m.status_id, s.note as status_name, m.code, m.name, m.abbreviation from measurement m join status s on (s.id = m.status_id) where m.id = :id';
   }

   protected function getFields(Request $request) {
      return ['code', 'name', 'abbreviation'];
   }
}

?>