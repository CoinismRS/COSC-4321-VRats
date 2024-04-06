package main

import (
	"net/http"
	"strings"

	"github.com/gin-gonic/gin"
)

type Color struct {
	Name  string `json:"name"`
	Brand string `json:"brand"`
	Code  string `json:"code"`
}

// DEMO, REMOVE LATER
var paintColors = []Color{
	{"Alabaster", "Sherwin-Williams", "#efefef"},
	{"Chantilly Lace", "Benjamin Moore", "#f8f7fc"},
	{"Polar Bear", "Behr Marquee", "#f0f0f0"},
}

func getAllColors(c *gin.Context) {
	c.JSON(http.StatusOK, paintColors)
}

func getAllColorsByBrand(c *gin.Context) {
	brand := c.Param("brand") // Get the brand name from the URL

	filteredColors := []Color{}
	for _, color := range paintColors {
		if strings.EqualFold(color.Brand, brand) {
			filteredColors = append(filteredColors, color)
		}
	}

	c.JSON(http.StatusOK, filteredColors)
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
