	<nav>
		<ul>
			<li><a href="<?=URL?>">&larr;</a></li>
			<li><a href="<?=URL?>post/add">новый пост</a></li>
			<li>пользователи</li>
			<li><a href="<?=URL?>tag">теги</a></li>
		</ul>
	</nav>
</header>
<main>
	<? if ($users): ?>
	<table>
		<? foreach ($users as $uid => $name): ?>
		
		<tr>
			<td><?=$name?></td>
			<td><a href="<?=URL?>user/edit/<?=$uid?>">редактировать</a></td>
			<td><a href="<?=URL?>user/del/<?=$uid?>">удалить</a></td>
		</tr>

		<? endforeach ?>
	</table>

	<? else: ?>

		<p><em>Увы, но пользователей нет.</em></p>

	<? endif ?>

	<p><a href="<?=URL?>user/add">Добавить</a></p>

</main>