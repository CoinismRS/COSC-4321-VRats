# N-terior Web API

This web API is written in Go and uses the Gin HTTP web framework to serve traffic.

## RESTful Endpoints

Get all colors from every brand
``` GET /colors ```

Get all colors from a specific brand
``` GET /colors/brand/:brand(string) ```

Get the specific color via name, will look through all color brands
``` GET /colors/:color(string)```

## Installation

The installation of this is straight forward and can be accomplished by simply running

``` go run main.go ```

You can also build for local deployement and create an executable

``` go build main.go ```

If you are deploying on Docker for production or development run

``` docker compose build ```

followed by

``` docker compose run ```
