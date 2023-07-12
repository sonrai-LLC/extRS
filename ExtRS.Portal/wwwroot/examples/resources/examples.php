<?php

if ( isset( $_POST['src'] ) && (
		$_POST['src'] === '../php/rest/get.php' ||
		$_POST['src'] === '../../controllers/rest/get.php' ||
		preg_match( '/^\.\.\/php\/[a-zA-Z_\-]+\.php$/', $_POST['src'] ) !== 0 ||
		preg_match( '/^\.\.\/\.\.\/controllers\/[a-zA-Z_\-]+\.php$/', $_POST['src'] ) !== 0
	) )
{
	echo file_get_contents( $_POST['src'] );
}
else {
	echo '';
}


