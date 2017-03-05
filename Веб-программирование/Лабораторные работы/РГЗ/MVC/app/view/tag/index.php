	<nav>
		<ul>
			<li><a href="<?=URL?>">&larr;</a></li>
			<li><a href="<?=URL?>post/add">новый пост</a></li>
			<li><a href="<?=URL?>user">пользователи</a></li>
			<li>теги</li>
		</ul>
	</nav>
</header>
<main>
	<? if ($tags): ?>
	<table>
		<? foreach ($tags as $uid => $name): ?>
		
		<tr>
			<td><?=$name?></td>
			<td><a href="<?=URL?>tag/edit/<?=$uid?>">редактировать</a></td>
			<td><a href="<?=URL?>tag/del/<?=$uid?>">удалить</a></td>
		</tr>

		<? endforeach ?>
	</table>

	<? else: ?>

		<p><em>Увы, но тегов нет.</em></p>

	<? endif ?>

	<p><a href="<?=URL?>tag/add">Добавить</a></p>

</main>