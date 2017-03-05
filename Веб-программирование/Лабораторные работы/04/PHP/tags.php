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
		if (empty($_POST['tag'])) {
			$errors[] = "Введите тег.";
		}

		if (empty($errors)) {
			$db->lastError = null;

			$tagdb = $db->Select("tags", array(
				'name' => $_POST['tag']
			));

			if ($tagdb) {
				$errors[] = "Такой тег уже существует.";
			}

			if (empty($errors)) {
				$db->Insert("tags", array(
					'name' => $_POST['tag']
				));

				if (! $db->lastError) {
					header('Location: ' . $config['index'] . 'tags');
				} else {
					$errors[] = "Ошибка при добавлении тега.";
					hdump($db->lastError);
				}
			}
		}
	}
}

if ($_GET['edit'] && is_numeric($_GET['edit'])) {
	$record = $db->Select("tags", array(
		'tag_id' => $_GET['edit']
	));

	$edit = array(
		'tag' => $record['name']
	);

	if ($_POST['save']) {
		if (empty($_POST['tag'])) {
			$errors[] = "Введите тег.";
		}

		if (empty($errors)) {
			$db->lastError = null;

			$db->Update("tags", array(
				'name' => $_POST['tag']
			), array(
				'tag_id' => $_GET['edit']
			));

			if (! $db->lastError) {
				header('Location: ' . $config['index'] . 'tags');
			} else {
				$errors[] = "Ошибка при изменении тега.";
				hdump($db->lastError);
			}
		}
	}
}

if ($_GET['del'] && is_numeric($_GET['del'])) {
	$record = $db->Select("tags", array(
		'tag_id' => $_GET['del']
	));

	if ($record) {
		$db->lastError = null;

		$db->Delete("tags", array(
			'tag_id' => $_GET['del']
		));

		$db->Delete("post_tags", array(
			'tag_id' => $_GET['del']
		));

		if (! $db->lastError) {
			header('Location: ' . $config['index'] . 'tags');
		}
	}
}

$tags = array();
$records = $db->Select('tags');

if (is_array($records[0])) {
	foreach ($records as $rec) {
		$tags[$rec['tag_id']] = $rec['name'];
	}
} else {
	$tags[$records['tag_id']] = $records['name'];
}

require_once('templs/tags.php');
?>