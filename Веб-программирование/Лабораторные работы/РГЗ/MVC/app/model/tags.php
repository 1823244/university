<?
class Tags extends Model
{
    /* 
     * Get all tags from DB
     */
    public function getList() {
        $tags = array();
        $records = $this->db->Select('tags');

        if (array_key_exists(0, $records)) {
            foreach ($records as $rec) {
                $tags[$rec['tag_id']] = $rec['name'];
            }
        } else {
            $tags[$records['tag_id']] = $records['name'];
        }

        return $tags;
    }

    /* 
     * Get tag from DB by ID
     */
    public function getByID($id) {
        return $this->db->Select('tags', array(
            'tag_id' => $id
        ));
    }

    /*
     * Check tag in DB
     */
    public function isExists($name) {
        return $this->db->Select('tags', array(
            'name' => $name
        ));
    }

    /*
     * Add tag
     */ 
    public function addTag($name)
    {
        $this->db->Insert('tags', array(
            'name' => $name
        ));
    }

    /*
     * Update tag
     */ 
    public function updateTag($id, $name)
    {
        $this->db->Update('tags', array(
            'name' => $name
        ), array(
            'tag_id' => $id
        ));
    }

    /*
     * Delete tag
     */
    public function deleteTag($id) {
        $this->db->Delete('tags', array(
            'tag_id' => $id
        ));
    }
}
?>