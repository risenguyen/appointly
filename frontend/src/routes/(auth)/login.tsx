import LoginForm from "@/features/auth/components/login-form";
import { createFileRoute, redirect } from "@tanstack/react-router";

export const Route = createFileRoute("/(auth)/login")({
  component: RouteComponent,
  beforeLoad({ context: { auth } }) {
    if (auth?.isAuthenticated) {
      throw redirect({
        to: "/app",
      });
    }
  },
  validateSearch: (search: { redirect?: string }) => search,
});

function RouteComponent() {
  return (
    <main className="flex h-dvh w-screen flex-col items-center justify-center">
      <div className="absolute top-4 left-6 text-lg font-medium">appointly</div>
      <div className="mb-auto pt-4"></div>
      <div className="w-full max-w-md px-6">
        <LoginForm />
      </div>
      <div className="text-muted-foreground mt-auto pb-3 text-sm">
        Made by Rise Nguyen
      </div>
    </main>
  );
}
