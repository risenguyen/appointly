import { router } from "@/App";
import { Client } from "@hey-api/client-fetch";
import { toast } from "sonner";

const API_URL = import.meta.env.VITE_API_URL;

function configureClient(client: Client) {
  client.setConfig({
    baseUrl: API_URL,
  });
  client.interceptors.request.use((request) => {
    const token = localStorage.getItem("token");
    const expiresAt = localStorage.getItem("expiresAt");

    if (request.url.includes("/login")) {
      return request;
    }

    if (!token || !expiresAt || Date.now() > Date.parse(expiresAt)) {
      toast("Session expired. Please log in again.");
      router.navigate({
        to: "/login",
      });
      throw request;
    }

    console.log("YES TOKEN", token);
    request.headers.set("Authorization", `Bearer ${token}`);
    return request;
  });
}

export { configureClient };
