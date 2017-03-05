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

if ($_POST['publish']) {
	$errors = array();
	
	$post = array(
		'title' => $_POST['title'],
		'text' => $_POST['post'],
		'tags' => $_POST['tags']
	);

	if (empty($_POST['title'])) {
		$errors[] = "Придумате заголовок для поста.";
	}

	if (empty($_POST['post'])) {
		$errors[] = "Напишите что-нибудь.";
	}

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
			$db->Insert('posts', array(
				'title' => $_POST['title'],
				'post' => $_POST['post'],
				'author_id' => $user['author_id'],
				'date' => date('Y-m-d H:i:s', time())
			));

			$postid = $db->GetLastInsertID();

			if ($_POST['tags']) {
				$tags = array_filter(array_unique(array_map('trim', explode(',', $_POST['tags']))));
				
				foreach ($tags as $tag) {
					$found = $db->Select('tags', array(
						'name' => $tag
					));

					if ($found) {
						$db->Insert('post_tags', array(
							'post_id' => $postid,
							'tag_id' => $found['tag_id']
						));
					} else {
						$db->Insert('tags', array(
							'name' => $tag
						));

						$tagid = $db->GetLastInsertID();

						$db->Insert('post_tags', array(
							'post_id' => $postid,
							'tag_id' => $tagid
						));
					}
				}
			}

			if (! $db->lastError) {
				header('Location: ' . $config['index']);
			} else {
				$errors[] = "Ошибка при добавлении поста.";
				hdump($db->lastError);
			}
		} else {
			$errors[] = "Авторизационные данные неверны.";
		}
	}
}

require_once('templs/new.php');
?>