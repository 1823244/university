<?
class Controller
{
    public $db = null;
    public $model = null;

    /**
     * whenever controller is created, open a database connection too and load "the model".
     */
    function __construct()
    {
        $this->connectDB();
        $this->loadModel();
    }

    /**
     * open the database connection with the credentials from application/config/config.php
     */
    private function connectDB()
    {
        // create a database connection
        $this->db = new DB(
                        DB_NAME,
                        DB_USER,
                        DB_PASS,
                        DB_HOST
                    );
    }

    /**
     * Loads the "model".
     */
    public function loadModel()
    {
        // for each child model:
        // $this->model = new Model($this->db);
    }
}
?>