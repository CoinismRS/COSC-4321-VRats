package main

import (
	"net/http"

	"github.com/gin-gonic/gin"
)

type Color struct {
	Name string `json:"name"`
	Code string `json:"code"`
}

// DEMO, REMOVE LATER
var paintColors = []Color{
	{"Alabaster", "#efefef"},
	{"Chantilly Lace", "#f8f7fc"},
	{"Polar Bear", "#f0f0f0"},
}

func getAllColors(c *gin.Context) {
	c.JSON(http.StatusOK, gin.H{
		"colors": paintColors,
	})
}

func getColorByColor(c *gin.Context) {

}

func main() {
	router := gin.Default()

	router.GET("/colors", getAllColors)
	router.GET("/colors/:color", getColorByColor)

	router.Run()
}
