<?
class Post extends Controller
{
    /**
     * PAGE: /post/@ID
     */
    public function byID($id)
    {
        if (!is_numeric($id)) header('Location: ' . URL . 'error');

        if ($_SERVER['REQUEST_METHOD'] == 'POST' && $_POST['submit']) {
            $errors = $this->checkComment();

            if (empty($errors)) {
                $this->model->addComment(
                    $_POST['name'],
                    $id,
                    $_POST['comment'],
                    $_POST['email']
                );
            }
        }

        $post = $this->model->getByID($id);

        if ($post == null) {
            header('Location: ' . URL . 'error');
        }

        $title = $post['title'];

        require APP . 'view/_templates/header.php';
        require APP . 'view/post/index.php';
        require APP . 'view/_templates/footer.php';
    }

    /**
     * PAGE: /post/add
     */
    public function add()
    {
        if ($_SERVER['REQUEST_METHOD'] == 'POST' && $_POST['publish']) {
            $errors = $this->checkPost();

            if (empty($errors)
                && $this->isUserExists($_POST['login'], $_POST['password'])) {
                $this->model->addPost(
                    $_POST['title'],
                    $_POST['post'],
                    $_POST['tags'],
                    $_POST['login'],
                    $_POST['password']
                ); 

                header('Location: ' . URL);
            } else {
                $errors[] = "Авторизационные данные неверны.";
            }
        }

        $title = 'Новый пост в Блог';

        require APP . 'view/_templates/header.php';
        require APP . 'view/post/add.php';
        require APP . 'view/_templates/footer.php';
    }

    /**
     * PAGE: /post/edit/@ID
     */
    public function edit($id)
    {
        if (!is_numeric($id)) header('Location: ' . URL . 'error');
           
        if ($_SERVER['REQUEST_METHOD'] == 'POST' && $_POST['publish']) {
            $errors = $this->checkPost();

            if (empty($errors)
                && $this->isUserExists($_POST['login'], $_POST['password'])) {
                $this->model->updatePost(
                    $id,
                    $_POST['title'],
                    $_POST['post'],
                    $_POST['tags'],
                    $_POST['login'],
                    $_POST['password']
                ); 
            } else {
                $errors[] = "Авторизационные данные неверны.";
            }

            $post = array(
                'id' => $id,
                'title' => $_POST['title'],
                'textRaw' => $_POST['post'],
                'tags' => $_POST['tags']
            );
        } else {
            $post = $this->model->getByID($id);

            if ($post == null) {
                header('Location: ' . URL . 'error');
            }
        }

        $title = 'Редактирование поста';

        require APP . 'view/_templates/header.php';
        require APP . 'view/post/edit.php';
        require APP . 'view/_templates/footer.php';
    }

    /**
     * PAGE: /post/del/@ID
     */
    public function del($id) {
        if (!is_numeric($id)) header('Location: ' . URL . 'error');

        if ($_SERVER['REQUEST_METHOD'] == 'POST' && $_POST['del']) {
            if (empty($_POST['login']) || empty($_POST['password'])) {
                $errors[] = "Введите авторизационные данные.";
            }

            if (empty($errors)
                && $this->isUserExists($_POST['login'], $_POST['password'])) {
                $this->model->deletePost($id); 
                header('Location: ' . URL);
            } else {
                $errors[] = "Авторизационные данные неверны.";
            }
        }

        $title = 'Удаление поста';
        $post = $this->model->getByID($id);

        require APP . 'view/_templates/header.php';
        require APP . 'view/post/del.php';
        require APP . 'view/_templates/footer.php';
    }

    private function checkComment()
    {
        $errors = array();

        if (empty($_POST['name'])) {
            $errors[] = "Вы не ввели имя.";
        }

        if (empty($_POST['email'])) {
            $errors[] = "Вы не ввели почту.";
        } elseif (! filter_var($_POST['email'], FILTER_VALIDATE_EMAIL)) {
            $errors[] = "Введённая электронная почта неверна.";
        }

        if (empty($_POST['comment'])) {
            $errors[] = "Вы не ввели текст комментария.";
        }

        return $errors;
    }

    private function checkPost()
    {
        $errors = array();

        if (empty($_POST['title'])) {
            $errors[] = "Придумате заголовок для поста.";
        }

        if (empty($_POST['post'])) {
            $errors[] = "Напишите что-нибудь.";
        }

        if (empty($_POST['login']) || empty($_POST['password'])) {
            $errors[] = "Введите авторизационные данные.";
        }

        return $errors;
    }

    private function isUserExists($login, $password) {
        return $this->db->Select('authors', array(
            'username' => $_POST['login'],
            'password' => $_POST['password']
        ));
    }

    public function loadModel()
    {
        require APP . 'model/posts.php';
        
        $this->model = new Posts($this->db);
    }
}
?>