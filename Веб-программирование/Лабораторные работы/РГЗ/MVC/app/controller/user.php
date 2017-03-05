<?
class User extends Controller
{
    /**
     * PAGE: /user/
     */
    public function index()
    {
        $title = 'Пользователи';
        $users = $this->model->getList();

        require APP . 'view/_templates/header.php';
        require APP . 'view/user/index.php';
        require APP . 'view/_templates/footer.php';
    }

    /**
     * PAGE: /user/add
     */
    public function add()
    {
        if ($_SERVER['REQUEST_METHOD'] == 'POST' && $_POST['save']) {
            $errors = $this->checkUser();

            if (empty($errors)) {
                if ($this->model->isExists($_POST['login'])) {
                    $errors[] = "Такой пользователи уже существует.";
                } else {
                    $this->model->addUser(
                        $_POST['login'],
                        $_POST['password']
                    ); 

                    header('Location: ' . URL . 'user');
                }
            }
        }

        $title = 'Новый пользователь';

        require APP . 'view/_templates/header.php';
        require APP . 'view/user/add.php';
        require APP . 'view/_templates/footer.php';
    }

    /**
     * PAGE: /user/edit/@ID
     */
    public function edit($id)
    {
        if (!is_numeric($id)) header('Location: ' . URL . 'error');
           
        if ($_SERVER['REQUEST_METHOD'] == 'POST' && $_POST['save']) {
            $errors = $this->checkUser();

            if (empty($errors)) {
                $this->model->updateUser(
                    $id,
                    $_POST['login'],
                    $_POST['password']
                ); 

                header('Location: ' . URL . 'user');
            }

            $user = array(
                'login' => $_POST['login'],
                'password' => $_POST['password']
            );
        } else {
            $user = $this->model->getByID($id);

            if ($user == null) {
                header('Location: ' . URL . 'error');
            }
        }

        $title = 'Редактирование пользователя';

        require APP . 'view/_templates/header.php';
        require APP . 'view/user/edit.php';
        require APP . 'view/_templates/footer.php';
    }

    /**
     * PAGE: /user/del/@ID
     */
    public function del($id) {
        if (!is_numeric($id)) header('Location: ' . URL . 'error');

        if ($_SERVER['REQUEST_METHOD'] == 'POST' && $_POST['del']) {
            $user = $this->model->getByID($id);

            if (empty($_POST['password'])) {
                $errors[] = "Введите пароль";
            } elseif ($_POST['password'] != $user['password']) {
                $errors[] = "Пароль неверен";
            }

            if (empty($errors)) {
                $this->model->deleteUser($id); 
                header('Location: ' . URL . 'user');
            }
        }

        $title = 'Удаление поста';
        $post = $this->model->getByID($id);

        require APP . 'view/_templates/header.php';
        require APP . 'view/user/del.php';
        require APP . 'view/_templates/footer.php';
    }

    private function checkUser()
    {
        $errors = array();

        if (empty($_POST['login'])) {
            $errors[] = "Введите логин.";
        }

        if (empty($_POST['password'])) {
            $errors[] = "Введите пароль.";
        }

        return $errors;
    }

    public function loadModel()
    {
        require APP . 'model/users.php';
        
        $this->model = new Users($this->db);
    }
}
?>