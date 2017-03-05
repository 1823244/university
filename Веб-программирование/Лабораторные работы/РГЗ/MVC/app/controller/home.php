<?
class Home extends Controller
{
    /**
     * PAGE: index
     */
    public function index()
    {
        $title = 'Блог';
        $posts = $this->model->getList(10);

        require APP . 'view/_templates/header.php';
        require APP . 'view/home/index.php';
        require APP . 'view/_templates/footer.php';
    }

    public function loadModel()
    {
        require APP . 'model/posts.php';
        
        $this->model = new Posts($this->db);
    }
}
?>