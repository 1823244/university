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

function getPost($rec, $authors) {
	return array(
		'link' => $config['index'] . 'post?id=' . $rec['post_id'],
 		'title' => $rec['title'],
		'author' => $authors[$rec['author_id']],
		'excerpt' => nl2br(strip_tags(mb_substr($rec['post'], 0, 200, 'UTF-8')) . '..'),
		'date' => date("d.m.y H:i", strtotime($rec['date']))
	);
}

$posts = array();
$records = $db->Select('posts', false, 'post_id DESC', '10');

$author_ids = array();

if (is_array($records[0])) {
	$author_ids = array_unique(array_map(function($o) {
									return $o['author_id'];
								}, $records));
} elseif ($records) {
	$author_ids[] = $records['author_id'];
}

$authors = array();

foreach ($author_ids as $author_id) {
	$rec = $db->Select('authors', array(
		'author_id' => $author_id
	));

	$authors[$author_id] = $rec['username'];
}

if (is_array($records[0])) {
	foreach ($records as $rec) {
		$posts[] = getPost($rec, $authors);
	}
} elseif($records) {
	$posts[] = getPost($records, $authors);
}

require_once('templs/index.php');
?>