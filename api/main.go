package main

import (
	"encoding/json"
	"fmt"
	"io"
	"net/http"
	"os"

	"github.com/gin-gonic/gin"
)

type Color struct {
	Name string `json:"name"`
	Code string `json:"hex"`
}

// DEMO, REMOVE LATER
var paintColors []Color

func getAllColors(c *gin.Context) {
	c.JSON(http.StatusOK, gin.H{
		"colors": paintColors,
	})
}

func getColorByColor(c *gin.Context) {

}

func main() {
	file, err := os.Open("colors.json")
	if err != nil {
		fmt.Println("Error opening file:", err)
		return
	}
	defer file.Close()

	// Read file content
	fileContent, err := io.ReadAll(file)
	if err != nil {
		fmt.Println("Error reading file:", err)
		return
	}

	// Unmarshal JSON data into the slice of Color
	err = json.Unmarshal(fileContent, &paintColors)
	if err != nil {
		fmt.Println("Error unmarshalling JSON:", err)
		return
	}

	router := gin.Default()

	router.GET("/colors", getAllColors)
	router.GET("/colors/:color", getColorByColor)

	router.Run()
}
