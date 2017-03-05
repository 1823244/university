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

if (isset($_GET['add'])) {
	$edit = true;

	if ($_POST['save']) {
		if (empty($_POST['login'])) {
				$errors[] = "Введите логин.";
		}

		if (empty($_POST['password'])) {
			$errors[] = "Введите пароль.";
		}

		if (empty($errors)) {
			$db->lastError = null;

			$userdb = $db->Select("authors", array(
				'username' => $_POST['login']
			));

			if ($userdb) {
				$errors[] = "Такой пользователь уже существует.";
			}

			if (empty($errors)) {
				$db->Insert("authors", array(
					'username' => $_POST['login'],
					'password' => $_POST['password']
				));

				if (! $db->lastError) {
					header('Location: ' . $config['index'] . 'users');
				} else {
					$errors[] = "Ошибка при добавлении пользователя.";
					hdump($db->lastError);
				}
			}
		}
	}
}

if ($_GET['edit'] && is_numeric($_GET['edit'])) {
	$record = $db->Select("authors", array(
		'author_id' => $_GET['edit']
	));

	$edit = array(
		'login' => $record['username'],
		'password' => $record['password']
	);

	if ($_POST['save']) {
		if (empty($_POST['login'])) {
			$errors[] = "Введите логин.";
		}

		if (empty($_POST['password'])) {
			$errors[] = "Введите пароль.";
		}

		if (empty($errors)) {
			$db->lastError = null;

			$db->Update("authors", array(
				'username' => $_POST['login'],
				'password' => $_POST['password']
			), array(
				'author_id' => $_GET['edit']
			));

			if (! $db->lastError) {
				header('Location: ' . $config['index'] . 'users');
			} else {
				$errors[] = "Ошибка при изменении пользователя.";
				hdump($db->lastError);
			}
		}
	}
}

if ($_GET['del'] && is_numeric($_GET['del'])) {
	$del = true;

	$record = $db->Select("authors", array(
		'author_id' => $_GET['del']
	));

	if (! $record) {
		$del = false;
	} elseif ($_POST['del']) {
		if (empty($_POST['password'])) {
			$errors[] = "Введите пароль.";
		} elseif ($_POST['password'] != $record['password']) {
			$errors[] = "Пароль неверен";
		}

		if (empty($errors)) {
			$db->lastError = null;

			$db->Delete("authors", array(
				'author_id' => $_GET['del']
			));

			if (! $db->lastError) {
				header('Location: ' . $config['index'] . 'users');
			} else {
				$errors[] = "Ошибка при удалении пользователя.";
				hdump($db->lastError);
			}
		}
	}
}

$users = array();
$records = $db->Select('authors');

if (is_array($records[0])) {
	foreach ($records as $rec) {
		$users[$rec['author_id']] = $rec['username'];
	}
} else {
	$users[$records['author_id']] = $records['username'];
}

require_once('templs/users.php');
?>