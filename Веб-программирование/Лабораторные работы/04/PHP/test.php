<?

function dump($data, $die = false) { 
    echo '<pre style="font-size:12px">' . nl2br(str_replace(' ', '&nbsp;', print_r($data, true))) . '</pre><hr>'; 
}

error_reporting(E_ALL);
ini_set('display_errors', 1);

$a = "12345";
$a[$a[0]] = "2";
echo $a;

?>
