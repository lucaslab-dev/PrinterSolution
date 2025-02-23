<div align="center" id="top"> 
  <img src="./.github/app.gif" alt="PrinterSolution" />

&#xa0;

  <!-- <a href="https://printersolution.netlify.app">Demo</a> -->
</div>

<h1 align="center">PrinterSolution</h1>

<p align="center">
  <img alt="Github top language" src="https://img.shields.io/github/languages/top/{{YOUR_GITHUB_USERNAME}}/printersolution?color=56BEB8">

  <img alt="Github language count" src="https://img.shields.io/github/languages/count/{{YOUR_GITHUB_USERNAME}}/printersolution?color=56BEB8">

  <img alt="Repository size" src="https://img.shields.io/github/repo-size/{{YOUR_GITHUB_USERNAME}}/printersolution?color=56BEB8">

  <img alt="License" src="https://img.shields.io/github/license/{{YOUR_GITHUB_USERNAME}}/printersolution?color=56BEB8">

  <!-- <img alt="Github issues" src="https://img.shields.io/github/issues/{{YOUR_GITHUB_USERNAME}}/printersolution?color=56BEB8" /> -->

  <!-- <img alt="Github forks" src="https://img.shields.io/github/forks/{{YOUR_GITHUB_USERNAME}}/printersolution?color=56BEB8" /> -->

  <!-- <img alt="Github stars" src="https://img.shields.io/github/stars/{{YOUR_GITHUB_USERNAME}}/printersolution?color=56BEB8" /> -->
</p>

<!-- Status -->

<!-- <h4 align="center">
	🚧  PrinterSolution 🚀 Under construction...  🚧
</h4>

<hr> -->

<p align="center">
  <a href="#dart-about">About</a> &#xa0; | &#xa0; 
  <a href="#sparkles-features">Features</a> &#xa0; | &#xa0;
  <a href="#rocket-technologies">Technologies</a> &#xa0; | &#xa0;
  <a href="#white_check_mark-requirements">Requirements</a> &#xa0; | &#xa0;
  <a href="#checkered_flag-starting">Starting</a> &#xa0; | &#xa0;
  <a href="#memo-license">License</a> &#xa0; | &#xa0;
  <a href="https://github.com/{{YOUR_GITHUB_USERNAME}}" target="_blank">Author</a>
</p>

<br>

## :dart: About

PrinterSolution is a distributed printing simulation system that demonstrates the integration between a Web API and a Console Application using RabbitMQ as a message broker. The system allows sending print commands through a REST API endpoint, which are then processed by a console application that simulates a remote printer.

## :sparkles: Features

:heavy_check_mark: REST API for receiving print requests;\
:heavy_check_mark: Message queuing with RabbitMQ for reliable message delivery;\
:heavy_check_mark: Console-based printer simulation;\
:heavy_check_mark: Asynchronous message processing;\
:heavy_check_mark: Scalable architecture for multiple printer support

## :rocket: Technologies

The following tools were used in this project:

- [.NET Core](https://dotnet.microsoft.com/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [ASP.NET Web API](https://dotnet.microsoft.com/apps/aspnet/apis)
- [Docker](https://www.docker.com/) (for RabbitMQ container)
- [Swagger/OpenAPI](https://swagger.io/)

## :white_check_mark: Requirements

Before starting :checkered_flag:, you need to have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com)

## :checkered_flag: Starting

```bash
# Clone this project
$ git clone https://github.com/{{YOUR_GITHUB_USERNAME}}/printersolution

# Access
$ cd printersolution

# Start RabbitMQ container
$ docker run -d --hostname my-rabbit --name some-rabbit \
  -p 5672:5672 -p 15672:15672 \
  -v rabbitmq_data:/var/lib/rabbitmq \
  rabbitmq:3-management

# Run the Web API (in one terminal)
$ cd WebAPI
$ dotnet run

# Run the Console Printer App (in another terminal)
$ cd PrinterConsole
$ dotnet run

# The Web API will be available at <http://localhost:5000>
# Swagger documentation can be accessed at <http://localhost:5000/swagger>
# RabbitMQ management interface will be at <http://localhost:15672>
```

## :memo: License

This project is under license from MIT. For more details, see the [LICENSE](LICENSE.md) file.

Made with :heart: by <a href="https://github.com/lucaslab-dev" target="_blank">Lucas Lab</a>

&#xa0;

<a href="#top">Back to top</a>
