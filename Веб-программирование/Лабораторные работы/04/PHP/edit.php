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

	if ($_POST['publish']) {
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

		if (empty($errors)) {
			$db->lastError = null;

			$user = $db->Select('authors', array(
				'username' => $_POST['login'],
				'password' => $_POST['password']
			));

			if ($user) {
				$db->Update('posts', array(
					'title' => $_POST['title'],
					'post' => $_POST['post'],
					'author_id' => $user['author_id'],
					'date' => date('Y-m-d H:i:s', time())
				), array(
					'post_id' => $_GET['id']
				));

				$postid = $_GET['id'];

				if ($_POST['tags']) {
					$tags = array_filter(array_unique(array_map('trim', explode(',', $_POST['tags']))));

					$db->Delete('post_tags', array(
						'post_id' => $postid
					));

					foreach ($tags as $tag) {
						$found = $db->Select('tags', array(
							'name' => $tag
						));

						if ($found) {
							$db->Insert('post_tags', array(
								'post_id' => $postid,
								'tag_id' => $found['tag_id']
							));

							if (strpos($db->lastError, 'Duplicate entry') !== false) {
								$db->lastError = null;
							}
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
					header('Location: ' . $config['index'] . 'post?id=' . $_GET['id']);
				} else {
					$errors[] = "Ошибка при изменении поста.";
					hdump($db->lastError);
				}
			} else {
				$errors[] = "Авторизационные данные неверны.";
			}
		}

		$post = array(
			'title' => $_POST['title'],
			'text' => $_POST['post'],
			'tags' => $_POST['tags']
		);
	} else {
		$record = $db->Select('posts', array(
			'post_id' => $_GET['id']
		));

		if ($record) {
			$post = array(
				'title' => $record['title'],
				'text' => $record['post']
			);

			$tag_ids = $db->Select('post_tags',
									array('post_id' => $_GET['id']),
									false,
									false,
									'tag_id');

			$tags = array();

			if (is_array($tag_ids[0])) {
				foreach ($tag_ids as $tag_id) {
					$tag = $db->Select('tags', array(
									'tag_id' => $tag_id['tag_id']
								));

					$tags[] = $tag['name'];
				}
			} elseif ($tag_ids) {
				$tag = $db->Select('tags', array(
								'tag_id' => $tag_ids['tag_id']
							));
				$tags[] = $tag['name'];
			}

			$post['tags'] = implode(', ', $tags);
		} else {
			header('Location: ' . $config['index']);
		}
	}
} else {
	header('Location: ' . $config['index']);
}

require_once('templs/new.php');
?>