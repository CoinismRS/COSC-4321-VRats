package main

import (
	"net/http"
	"github.com/gin-gonic/gin"
)

type Color struct {
	Name string 'json:"name"'
	Brand string 'json:"brand"'
	Code string 'json:"code"'
}

type User struct {
	Name string 'json:"name"'
}

// DEMO, REMOVE LATER
var paintColors = []PaintColor{
	{"Alabaster", "Sherwin-Williams", "#efefef"},
	{"Chantilly Lace", "Benjamin Moore", "#f8f7fc"},
	{"Polar Bear", "Behr Marquee", "#f0f0f0"},
}

func getAllColors(c *gin.Context) {

}

func getAllColorsByBrand(c *gin.Context) {

}

func getColorByColor(c *gin.Context) {

}

func main() {
	router := gin.Default()

	router.GET("/colors", getAllColors)
	router.GET("/colors/brand/:brand", getAllColorsByBrand)
	router.GET("/colors/:color", getColorByColor)

	router.Run()
}