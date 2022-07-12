package com.example.demo;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;
import io.opentelemetry.extension.annotations.WithSpan;

@Service
public class DotNetCallService {

    @Value("${baseUrl}")
    private String baseUrl;
    
    @Autowired
    private RestTemplate restTemplate;
    
    @WithSpan
    public Weather[] getWeather() {
        ResponseEntity<Weather[]> response = restTemplate.getForEntity(
                baseUrl+ "WeatherForecast",
                Weather[].class);
        Weather[] data = response.getBody();
        return data;
    }
}
