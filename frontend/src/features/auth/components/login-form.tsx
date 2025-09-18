import { useAuth } from "@/context/auth-context";
import { useNavigate, useSearch } from "@tanstack/react-router";
import { useForm } from "react-hook-form";

import { LoaderCircle } from "lucide-react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";

type LoginInput = {
  email: string;
  password: string;
};

function LoginForm() {
  const { login, isLoading } = useAuth();

  const navigate = useNavigate();
  const search = useSearch({
    from: "/(auth)/login",
  });

  const form = useForm<LoginInput>({
    defaultValues: {
      email: "",
      password: "",
    },
  });

  async function onSubmit({ email, password }: LoginInput) {
    const { error } = await login(email, password);

    if (!error) {
      const path = search.redirect ? new URL(search.redirect).pathname : "/app";
      navigate({
        from: "/login",
        to: path,
      });
    }

    if (error) {
      form.setError("email", {
        type: "manual",
        message: "Invalid email or password.",
      });
      form.setError("password", {
        type: "manual",
        message: "Invalid email or password.",
      });
    }
  }

  return (
    <Form {...form}>
      <form className="space-y-6" onSubmit={form.handleSubmit(onSubmit)}>
        <FormField
          control={form.control}
          name="email"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Email</FormLabel>
              <FormControl>
                <Input
                  autoComplete="email"
                  placeholder="Enter your email"
                  {...field}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="password"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Password</FormLabel>
              <FormControl>
                <Input
                  type="password"
                  placeholder="Enter your password"
                  {...field}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button className="w-full text-sm">
          {isLoading ? <LoaderCircle className="animate-spin" /> : null}
          Login
        </Button>
      </form>
    </Form>
  );
}

export default LoginForm;
