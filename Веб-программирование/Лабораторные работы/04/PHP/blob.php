<?
function dump($data, $die = false) { 
	echo '<pre style="font-size:12px">' . nl2br(str_replace(' ', '&nbsp;', print_r($data, true))) . '</pre><hr>'; 

	if ($die) die;
}

function hdump($data) { 
	echo '<!--
    ' . print_r($data, true) . '
    -->'; 
}
?>