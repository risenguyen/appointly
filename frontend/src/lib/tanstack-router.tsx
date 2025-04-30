import { JSX } from "react";
import type { ErrorComponentProps } from "@tanstack/react-router";

type HandleErrorComponentParams<T> = {
  errorComponent: (
    props: Omit<ErrorComponentProps, "error"> & {
      error: T;
    },
  ) => JSX.Element;
  props: ErrorComponentProps;
};

function handleErrorComponent<T>({
  errorComponent,
  props: { error, reset, info },
}: HandleErrorComponentParams<T>): JSX.Element {
  if (error instanceof Error) {
    throw error;
  }
  return errorComponent({
    error: error as T,
    reset,
    info,
  });
}

type HandleRootErrorComponentParams = {
  errorComponent: (props: ErrorComponentProps) => JSX.Element;
  props: ErrorComponentProps;
};

function handleRootErrorComponent({
  errorComponent,
  props,
}: HandleRootErrorComponentParams): JSX.Element {
  return errorComponent({
    ...props,
  });
}

export { handleErrorComponent, handleRootErrorComponent };
