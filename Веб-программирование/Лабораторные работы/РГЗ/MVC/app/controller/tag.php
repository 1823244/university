<?
class Tag extends Controller
{
    /**
     * PAGE: /tag/
     */
    public function index()
    {
        $title = 'Теги';
        $tags = $this->model->getList();

        require APP . 'view/_templates/header.php';
        require APP . 'view/tag/index.php';
        require APP . 'view/_templates/footer.php';
    }

    /**
     * PAGE: /tag/add
     */
    public function add()
    {
        if ($_SERVER['REQUEST_METHOD'] == 'POST' && $_POST['save']) {
            $errors = $this->checkTag();

            if (empty($errors)) {
                if ($this->model->isExists($_POST['tag'])) {
                    $errors[] = "Такой тег уже существует.";
                } else {
                    $this->model->addTag(
                        $_POST['tag']
                    ); 

                    header('Location: ' . URL . 'tag');
                }
            }
        }

        $title = 'Новый тег';

        require APP . 'view/_templates/header.php';
        require APP . 'view/tag/add.php';
        require APP . 'view/_templates/footer.php';
    }

    /**
     * PAGE: /tag/edit/@ID
     */
    public function edit($id)
    {
        if (!is_numeric($id)) header('Location: ' . URL . 'error');
           
        if ($_SERVER['REQUEST_METHOD'] == 'POST' && $_POST['save']) {
            $errors = $this->checkTag();

            if (empty($errors)) {
                $this->model->updateTag(
                    $id,
                    $_POST['tag']
                ); 

                header('Location: ' . URL . 'tag');
            }

            $tag = array(
                'name' => $_POST['tag']
            );
        } else {
            $tag = $this->model->getByID($id);

            if ($tag == null) {
                header('Location: ' . URL . 'error');
            }
        }

        $title = 'Редактирование тега';

        require APP . 'view/_templates/header.php';
        require APP . 'view/tag/edit.php';
        require APP . 'view/_templates/footer.php';
    }

    /**
     * PAGE: /tag/del/@ID
     */
    public function del($id) {
        if (!is_numeric($id)) header('Location: ' . URL . 'error');

        $tag = $this->model->getByID($id);

        if ($tag) {
            $this->model->deleteTag($id);
            header('Location: ' . URL . 'tag');
        } else {
            header('Location: ' . URL . 'error');
        }
    }

    private function checkTag()
    {
        $errors = array();

        if (empty($_POST['tag'])) {
            $errors[] = "Введите тег.";
        }

        return $errors;
    }

    public function loadModel()
    {
        require APP . 'model/tags.php';
        
        $this->model = new Tags($this->db);
    }
}
?>