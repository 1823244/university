<?
class Posts extends Model
{
    /*
     * Convert DB strings to post array
     */
    function preparePost($rec, $authors) {
        return array(
            'id' => $rec['post_id'],
            'link' => URL . 'post/' . $rec['post_id'],
            'title' => $rec['title'],
            'author' => array_key_exists('username', $authors)
                        ? $authors['username']
                        : $authors[$rec['author_id']],
            'excerpt' => nl2br(strip_tags(mb_substr($rec['post'], 0, 200, 'UTF-8')) . '..'),
            'text' => '<p>' . str_replace('<br />', '</p><p>', nl2br($rec['post'])) . '</p>',
            'textRaw' => $rec['post'],
            'date' => date("d.m.y H:i", strtotime($rec['date']))
        );
    }

    /*
     * Convert DB strings to comment array
     */
    function prepareComment($rec) {
        return array(
            'name' => $rec['name'],
            'text' => '<p>' . str_replace('<br />', '</p><p>', nl2br($rec['comment'])) . '</p>',
            'email' => $rec['email'],
            'date' => date("d.m.y H:i", strtotime($rec['date']))
        );
    }

    /* 
     * Get last $n posts from DB
     */
    public function getList($n) {
        $posts = array();
        $records = $this->db->Select('posts', false, 'post_id DESC', $n);

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
            $rec = $this->db->Select('authors', array(
                'author_id' => $author_id
            ));

            $authors[$author_id] = $rec['username'];
        }

        if (is_array($records[0])) {
            foreach ($records as $rec) {
                $posts[] = $this->preparePost($rec, $authors);
            }
        } elseif($records) {
            $posts[] = $this->preparePost($records, $authors);
        }

        return $posts;
    }

    /* 
     * Get post from DB by ID
     */
    public function getByID($id) {
        $record = $this->db->Select('posts', array(
            'post_id' => $id
        ));

        if ($record) {
            $author = $this->db->Select('authors', array(
                'author_id' => $record['author_id']
            ));

            $post = $this->preparePost($record, $author);

            $dbcomments = $this->db->Select('comments', array('post_id' => $id));
            $comments = array();

            if (!empty($dbcomments)) {
                if (is_array($dbcomments[0])) {
                    foreach ($dbcomments as $dbcomment) {
                        $comments[] = $this->prepareComment($dbcomment);
                    }
                } else {
                    $comments[] = $this->prepareComment($dbcomments);
                }
            }
            
            $tag_ids = $this->db->Select(
                'post_tags',
                array('post_id' => $id),
                false,
                false,
                'tag_id'
            );

            $tags = array();

            if (array_key_exists(0, $tag_ids)) {
                foreach ($tag_ids as $tag_id) {
                    $tag = $this->db->Select('tags', array(
                        'tag_id' => $tag_id['tag_id']
                    ));

                    $tags[] = $tag['name'];
                }
            } elseif ($tag_ids) {
                $tag = $this->db->Select('tags', array(
                                'tag_id' => $tag_ids['tag_id']
                            ));
                $tags[] = $tag['name'];
            }

            $post['tags'] = implode(', ', $tags);
            $post['comments'] = $comments;

            return $post;
        } else {
            return null;
        }
    }

    /*
     * Add comment to post with ID == $postid
     */
    public function addComment($name, $postid, $comment, $email) {
        $this->db->Insert('comments', array(
            'name' => $name,
            'post_id' => $postid,
            'comment' => $comment,
            'email' => $email,
            'date' => date('Y-m-d H:i:s', time())
        ));
    }

    /*
     * Add post
     */ 
    public function addPost($title, $text, $tags, $login, $password)
    {
        $user = $this->db->Select('authors', array(
            'username' => $login,
            'password' => $password
        ));

        $this->db->Insert('posts', array(
            'title' => $title,
            'post' => $text,
            'author_id' => $user['author_id'],
            'date' => date('Y-m-d H:i:s', time())
        ));

        $id = $this->db->GetLastInsertID();

        if (!empty($tags)) {
            $tags = array_filter(array_unique(array_map('trim', explode(',', $tags))));
            
            foreach ($tags as $tag) {
                $found = $this->db->Select('tags', array(
                    'name' => $tag
                ));

                if ($found) {
                    $this->db->Insert('post_tags', array(
                        'post_id' => $id,
                        'tag_id' => $found['tag_id']
                    ));
                } else {
                    $this->db->Insert('tags', array(
                        'name' => $tag
                    ));

                    $tagid = $this->db->GetLastInsertID();

                    $this->db->Insert('post_tags', array(
                        'post_id' => $id,
                        'tag_id' => $tagid
                    ));
                }
            }
        }
    }

    /*
     * Update post
     */ 
    public function updatePost($id, $title, $text, $tags, $login, $password)
    {
        $user = $this->db->Select('authors', array(
            'username' => $login,
            'password' => $password
        ));

        $this->db->Update('posts', array(
            'title' => $title,
            'post' => $text,
            'author_id' => $user['author_id'],
            'date' => date('Y-m-d H:i:s', time())
        ), array(
            'post_id' => $id
        ));

        if (!empty($tags)) {
            $tags = array_filter(array_unique(array_map('trim', explode(',', $tags))));
            
            $this->db->Delete('post_tags', array(
                'post_id' => $id
            ));

            foreach ($tags as $tag) {
                $found = $this->db->Select('tags', array(
                    'name' => $tag
                ));

                if ($found) {
                    $this->db->Insert('post_tags', array(
                        'post_id' => $id,
                        'tag_id' => $found['tag_id']
                    ));
                } else {
                    $this->db->Insert('tags', array(
                        'name' => $tag
                    ));

                    $tagid = $this->db->GetLastInsertID();

                    $this->db->Insert('post_tags', array(
                        'post_id' => $id,
                        'tag_id' => $tagid
                    ));
                }
            }
        }
    }

    /*
     * Delete post
     */
    public function deletePost($id) {
        $this->db->Delete('posts', array(
            'post_id' => $id
        ));
    }
}
?>