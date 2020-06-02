<?php
// Copyright © 2018-2020 Тепляшин Сергей Васильевич. Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0

namespace App\Controller;

use Psr\Http\Message\ServerRequestInterface as Request;

class UsersController extends DatasetController {
   protected function getSQLQuery(Request $request) {
      return 'select id, name, surname, first_name, middle_name from user_alias where not is_system';
   }

   protected function getSQLQueryById(Request $request) {
      return 'select id, name, surname, first_name, middle_name from user_alias where not is_system and id = :id';
   }

}

?>