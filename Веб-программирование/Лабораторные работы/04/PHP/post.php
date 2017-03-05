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

function getComment($rec) {
	return array(
		'name' => $rec['name'],
		'text' => '<p>' . str_replace('<br />', '</p><p>', nl2br($rec['comment'])) . '</p>',
		'email' => $rec['email'],
		'date' => date("d.m.y H:i", strtotime($rec['date']))
	);
}

if ($_GET['id'] && is_numeric($_GET['id'])) {

	if ($_POST['submit']) {
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

		if (empty($errors)) {
			$db->Insert('comments', array(
				'name' => $_POST['name'],
				'post_id' => $_GET['id'],
				'comment' => $_POST['comment'],
				'email' => $_POST['email'],
				'date' => date('Y-m-d H:i:s', time())
			));
		}
	}

	$record = $db->Select('posts', array(
		'post_id' => $_GET['id']
	));

	if ($record) {
		$author = $db->Select('authors', array(
			'author_id' => $record['author_id']
		));

		$post = array(
			'title' => $record['title'],
			'author' => $author['username'],
			'text' => '<p>' . str_replace('<br />', '</p><p>', nl2br($record['post'])) . '</p>',
			'date' => date("d.m.y H:i", strtotime($record['date']))
		);

		$dbcomments = $db->Select('comments', array('post_id' => $_GET['id']));
		$comments = array();

		if (is_array($dbcomments[0])) {
			foreach ($dbcomments as $dbcomment) {
				$comments[] = getComment($dbcomment);
			}
		} elseif ($dbcomments) {
			$comments[] = getComment($dbcomments);
		}

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
} else {
	header('Location: ' . $config['index']);
}

require_once('templs/post.php');
?>