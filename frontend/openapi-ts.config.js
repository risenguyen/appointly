import { defineConfig } from "@hey-api/openapi-ts";

export default defineConfig({
  input: "https://localhost:7033/openapi/v1.json",
  output: {
    path: "src/api",
    format: "prettier",
    lint: "eslint",
  },
  plugins: ["@hey-api/client-fetch"],
});
