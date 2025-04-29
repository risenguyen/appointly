import { type ErrorComponentProps } from "@tanstack/react-router";
import { ReactNode } from "react";

function handleErrorComponent<T>(
  handler: (
    props: { problemDetails: T } & Omit<ErrorComponentProps, "error">,
  ) => ReactNode,
) {
  return function ({
    error,
    reset,
    info,
  }: Omit<ErrorComponentProps, "error"> & {
    error: unknown;
  }) {
    if (error instanceof Error) {
      throw error;
    }
    const problemDetails = error as T;
    return handler({
      problemDetails,
      reset,
      info,
    });
  };
}

function handleRootErrorComponent() {}

export { handleErrorComponent, handleRootErrorComponent };
