package com.example.demo;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

@RestController
public class WeatherController {

    @Autowired
    private RestTemplate restTemplate;

    @Value("${baseUrl}")
    private String baseUrl;

    @GetMapping("/weather")
    public Weather weather() {
        ResponseEntity<Weather[]> response = restTemplate.getForEntity(
                baseUrl+ "WeatherForecast",
                Weather[].class);
        Weather[] data = response.getBody();

        return data[0];
    }
}
