import { Client } from "@hey-api/client-fetch";

const API_URL = import.meta.env.VITE_API_URL;

function configureClient(client: Client) {
  client.setConfig({
    baseUrl: API_URL,
  });
}

export { configureClient };
