<?
require_once('blob.php');
require_once('db.php');
require_once('config.php');

$db = new DB(
	$config['dbname'],
	$config['dbuser'],
	$config['dbpassword'],
	$config['dbhost']
);

if ($_GET['id'] && is_numeric($_GET['id'])) {

	if ($_POST['del']) {
		$errors = array();

		if (empty($_POST['login']) || empty($_POST['password'])) {
			$errors[] = "Введите авторизационные данные.";
		}

		if (empty($errors)) {
			$db->lastError = null;

			$user = $db->Select('authors', array(
				'username' => $_POST['login'],
				'password' => $_POST['password']
			));

			if ($user) {
				$db->Delete('posts', array(
					'post_id' => $_GET['id']
				));

				if (! $db->lastError) {
					header('Location: ' . $config['index']);
				} else {
					$errors[] = "Ошибка при удалении поста.";
					hdump($db->lastError);
				}
			} else {
				$errors[] = "Авторизационные данные неверны.";
			}
		}
	}


	$record = $db->Select('posts', array(
		'post_id' => $_GET['id']
	), false, false, 'title');

	if ($record) {
		$post = array(
			'title' => $record['title']
		);
	} else {
		header('Location: ' . $config['index']);
	}
} else {
	header('Location: ' . $config['index']);
}

require_once('templs/del.php');
?>