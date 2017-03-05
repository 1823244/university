    <nav>
        <ul>
            <li><a href="<?=URL?>">←</a></li>
            <li><a href="<?=URL?>user">пользователи</a></li>
            <li><a href="<?=URL?>tag">теги</a></li>
        </ul>
    </nav>
</header>
<main>

    <article>
        <?=$post['text']?>
        <br>
        <p>
            <em>
                <?=$post['tags']?>
                <br>
                <?=$post['author']?>, <?=$post['date']?>
                (<a href="<?=URL?>post/edit/<?=$post['id']?>">редактировать</a>,
                <a href="<?=URL?>post/del/<?=$post['id']?>">удалить</a>)
            </em>
        </p>
    </article>

    <section>
        <h3>Комментарии <?=(count($post['comments'])) ? '(' . count($post['comments']) . ')' : ''?></h3>
        <br>
        
        <? foreach ($post['comments'] as $comment): ?>
        <article>
            <p><em><?=$comment['name']?> (<?=$comment['email']?>), <?=$comment['date']?></em></p>
            <?=$comment['text']?>
        </article>
        <? endforeach ?>
        
        <form method="POST" id="comment">
            <fieldset>
                <legend>Оставьте свой комментарий</legend>

                <? if (!empty($errors)): ?>
                    <? foreach ($errors as $error): ?>
                    <p><mark><?=$error?></mark></p>
                    <? endforeach; ?>
                <? endif ?>

                <input type="text" name="name" placeholder="Имя" required>
                <input type="email" name="email" placeholder="Электронная почта" required>
                <textarea name="comment" id="comment" rows="4" placeholder="Комментарий" required></textarea>
                <input type="submit" name="submit" id="comment-submit" value="Отправить">
                <script>
                    window.onkeyup = function(e) {
                        if ((e.keyCode == 10 || e.keyCode == 13) && e.ctrlKey) {
                            document.getElementById('comment-submit').click();
                        }
                    }

                    <? if (!empty($errors)): ?>
                    window.onload = function() {
                        var comments = document.getElementById('comment');

                        window.scrollTo(0, comments.offsetTop);
                    }
                    <? endif ?>
                </script>
            </fieldset>
        </form>
    </section>

</main>