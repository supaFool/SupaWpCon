# SupaWpCon
 Wordpress implementation into C# projects
 
 ## Wordpress site requirements
 * Install [WP REST API](https://github.com/WP-API/WP-API) plugin. 
 * Install [JWT Authentication for WP REST API](https://wordpress.org/plugins/jwt-authentication-for-wp-rest-api/) plugin.

Then to complete JWT configuration it is necessary to add some modifications to the .htaaccess file:

First, enable HTTP Authorization Header adding the following:<br>
`RewriteEngine on`<br>
`RewriteCond %{HTTP:Authorization} ^(.*)`<br>
`RewriteRule ^(.*) - [E=HTTP_AUTHORIZATION:%1]`<br>

Then enable the WPENGINE by adding this code to the same .htacess file:<br>
`SetEnvIf Authorization "(.*)" HTTP_AUTHORIZATION=$1`

JWT needs a secret key to sign the token this secret key must be unique and never revealed.

To add the secret key edit your wp-config.php file and add a new constant called `JWT_AUTH_SECRET_KEY`.<br>

`define('JWT_AUTH_SECRET_KEY', 'your-top-secrect-key');`<br>

You can generate and use a string from here https://api.wordpress.org/secret-key/1.1/salt/
