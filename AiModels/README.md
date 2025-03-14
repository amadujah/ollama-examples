# Console Application to run ollama with docker using microsoft AI extensions
More examples are available here https://github.com/dotnet/ai-samples/blob/main/src/microsoft-extensions-ai/ollama/OllamaExamples/README.md

## Steps to Run the App

### 1. Run Ollama Model

Ensure your Docker environment is ready and follow these steps:

1. **Start Ollama with Docker Compose**:

```sh
   docker-compose up -d
```
### 2. Load Your Model:

Execute the following command to load a model into the running Ollama container:
   ```sh
    docker exec -it ollama ollama run model-name
```

Models are available here: https://ollama.com/library.

### 3. Now your can run your console application to chat with your assistant
