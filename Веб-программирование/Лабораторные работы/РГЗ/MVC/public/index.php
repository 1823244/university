<?
// set a constant that holds the project's folder path, like "/var/www/".
// DIRECTORY_SEPARATOR adds a slash to the end of the path
define('ROOT', dirname(__DIR__) . DIRECTORY_SEPARATOR);
// set a constant that holds the project's "app" folder, like "/var/www/app".
define('APP', ROOT . 'app' . DIRECTORY_SEPARATOR);

// load application config
require APP . 'config/config.php';

// load class for DB CRUD
require APP . 'libs/db.php';

// load helpers
require APP . 'libs/helper.php';

// load application class
require APP . 'core/app.php';

// load model class
require APP . 'core/model.php';

// load controller class
require APP . 'core/controller.php';

// start the application
$app = new Application();
?>