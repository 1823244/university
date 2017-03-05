<?
class Error extends Controller
{
    /**
     * PAGE: index
     */
    public function index()
    {   
        $title = '404';
        
        require APP . 'view/_templates/header.php';
        require APP . 'view/error/index.php';
        require APP . 'view/_templates/footer.php';
    }
}
?>