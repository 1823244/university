<?
/* DB */
class DB {
	
    public  $lastError;         
	public  $lastQuery;         
	public  $result;            
	public  $records;           
	public  $affected;          
	public  $rawResults;        
	public  $arrayedResult;     
	
	private $hostname;          
	private $username;          
	private $password;          
	private $database;          
	
	private $databaseLink;      
	
	function __construct($database, $username, $password, $hostname = 'localhost') {
		$this->database = $database;
		$this->username = $username;
		$this->password = $password;
		$this->hostname = $hostname;
		
		$this->Connect();
	}
	
	function __destruct() {
		$this->CloseConnection();
	}	
	
	// Create connection
	private function Connect() {
		$this->CloseConnection();
		
		$this->databaseLink = mysqli_connect($this->hostname, $this->username, $this->password);
		
		if (!$this->databaseLink) {
   			$this->lastError = 'Can`t connect to server: ' . mysqli_error($this->databaseLink);

			return false;
		}
		
		if (!$this->SelectDB()) {
			$this->lastError = 'Can`t connect to database: ' . mysqli_error($this->databaseLink);

			return false;
		}
		
		$this->SetCharset();

		return true;
	}
	
	// Select database to use
	private function SelectDB() {
		if (!mysqli_select_db($this->databaseLink, $this->database)) {
			$this->lastError = 'Can`t select database: ' . mysqli_error($this->databaseLink);
			
			return false;
		} else {
			return true;
		}
	}
	
	// Performs a 'mysqli_real_escape_string' on the entire array/string
	private function SecureData($data) {
		if (is_array($data)) {
            $i = 0;
			
			foreach ($data as $key => $val) {
				if (!is_array($data[$key])) {
					$data[$key] = mysqli_real_escape_string($this->databaseLink, $data[$key]);
                    $i++;
				}
			}
		} else {
			$data = mysqli_real_escape_string($this->databaseLink, $data);
		}

		return $data;
	}

    // Executes MySQL query
    public function Execute($query) {
        $this->lastQuery = $query;

        if ($this->result = mysqli_query($this->databaseLink, $query)) {
            if (gettype($this->result) === 'object') {
                $this->records = @mysqli_num_rows($this->result);
            } else {
               $this->records = 0;
            }

            $this->affected = @mysqli_affected_rows($this->databaseLink);

            if ($this->records > 0) {
                $this->ArrayResults();

                return $this->arrayedResult;
            } else {
                return true;
            }

        } else {
            $this->lastError = mysqli_error($this->databaseLink);
            
            return false;
        }
    }

	public function Commit() {
		return mysqli_query($this->databaseLink, "COMMIT");
	}
  
	public function Rollback() {
		return mysqli_query("ROLLBACK", $this->databaseLink);
	}

	public function SetCharset($charset = 'UTF8') {
		return mysqli_set_charset($this->databaseLink, $this->SecureData($charset));
	}
	
    // Adds a record to the database based on the array key names
    public function Insert($table, $vars) {
        $vars = $this->SecureData($vars);

        $query = "INSERT INTO `{$table}` SET ";
        
        foreach ($vars as $key => $value) {
            $query .= "`{$key}` = '{$value}', ";
        }

        $query = trim($query, ', ');

        return $this->Execute($query);
    }

    // Deletes a record from the database
    public function Delete($table, $where = '', $limit = '') {
        $query = "DELETE FROM `{$table}` WHERE ";
        if (is_array($where) && $where != '') {
            $where = $this->SecureData($where);

            foreach ($where as $key => $value) {
            	$query .= "`{$key}` = '{$value}' AND ";
            }

            $query = substr($query, 0, -5);
        }

        if ($limit != '') {
            $query .= ' LIMIT ' . $limit;
        }

        return $this->Execute($query);
    }

    // Gets a single row from $from where $where is true
    public function Select($from, $where = '', $orderBy = '', $limit = '', $cols = '*', $operand = 'AND') {
        if (trim($from) == '') {
            return false;
        }

        $query = "SELECT {$cols} FROM `{$from}` WHERE ";

        if (is_array($where) && $where != '') {
            $where = $this->SecureData($where);

            foreach ($where as $key => $value) {
                $query .= "`{$key}` = '{$value}' {$operand} ";
            }

            $query = substr($query, 0, -(strlen($operand)+2));

        } else {
            $query = substr($query, 0, -6);
        }

        if ($orderBy != '') {
            $query .= ' ORDER BY ' . $orderBy;
        }

        if ($limit != '') {
            $query .= ' LIMIT ' . $limit;
        }

        $result = $this->Execute($query);
        
        if (is_array($result)) return $result;

        return array();
    }

    // Updates a record in the database based on WHERE
    public function Update($table, $set, $where) {
        if (trim($table) == '' || !is_array($set) || !is_array($where)) {
            return false;
        }

        $set 	= $this->SecureData($set);
        $where 	= $this->SecureData($where);

        $query = "UPDATE `{$table}` SET ";

        foreach ($set as $key => $value) {
            $query .= "`{$key}` = '{$value}', ";
        }

        $query = substr($query, 0, -2);

        $query .= ' WHERE ';

        foreach ($where as $key => $value) {
            $query .= "`{$key}` = '{$value}' AND ";
        }

        $query = substr($query, 0, -5);

        return $this->Execute($query);
    }

    // 'Arrays' a single result
    public function ArrayResult() {
        $this->arrayedResult = mysqli_fetch_assoc($this->result) or die (mysqli_error($this->databaseLink));
        return $this->arrayedResult;
    }

    // 'Arrays' multiple result
    public function ArrayResults() {
        if ($this->records == 1) {
            return $this->ArrayResult();
        }

        $this->arrayedResult = array();

        while ($data = mysqli_fetch_assoc($this->result)) {
            $this->arrayedResult[] = $data;
        }
        
        return $this->arrayedResult;
    }

    // 'Arrays' multiple results with a key
    public function ArrayResultsWithKey($key = 'id') {
        if (isset($this->arrayedResult)) {
            unset($this->arrayedResult);
        }

        $this->arrayedResult = array();
        
        while($row = mysqli_fetch_assoc($this->result)) {
            foreach ($row as $theKey => $theValue) {
                $this->arrayedResult[$row[$key]][$theKey] = $theValue;
            }
        }
        
        return $this->arrayedResult;
    }

    // Returns last insert ID
    public function GetLastInsertID() {
        return mysqli_insert_id($this->databaseLink);
    }

    // Returns number of rows
    public function CountRows($from, $where = '') {
        $result = $this->Select($from, $where, '', '', 'count(*)');

        return $result["count(*)"];
    }

    // Closes the connections
    public function CloseConnection() {
        if ($this->databaseLink) {
        	
        	// you never know, man :3
			$this->Commit();

            mysqli_close($this->databaseLink);
        }
    }
}
?>