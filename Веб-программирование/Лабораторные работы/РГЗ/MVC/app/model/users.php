<?
class Users extends Model
{
    /* 
     * Get all users from DB
     */
    public function getList() {
        $users = array();
        $records = $this->db->Select('authors');

        if (array_key_exists(0, $records)) {
            foreach ($records as $rec) {
                $users[$rec['author_id']] = $rec['username'];
            }
        } else {
            $users[$records['author_id']] = $records['username'];
        }

        return $users;
    }

    /* 
     * Get user from DB by ID
     */
    public function getByID($id) {
        $record = $this->db->Select('authors', array(
            'author_id' => $id
        ));

        return array(
            'login' => $record['username'],
            'password' => $record['password']
        );
    }

    /*
     * Check user in DB
     */
    public function isExists($login) {
        return $this->db->Select('authors', array(
            'username' => $login
        ));
    }

    /*
     * Add user
     */ 
    public function addUser($login, $password)
    {
        $this->db->Insert('authors', array(
            'username' => $login,
            'password' => $password
        ));
    }

    /*
     * Update user
     */ 
    public function updateUser($id, $login, $password)
    {
        $this->db->Update('authors', array(
            'username' => $login,
            'password' => $password
        ), array(
            'author_id' => $id
        ));
    }

    /*
     * Delete user
     */
    public function deleteUser($id) {
        $this->db->Delete('authors', array(
            'author_id' => $id
        ));
    }
}
?>