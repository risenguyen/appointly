import {
  createContext,
  ReactNode,
  useCallback,
  useContext,
  useEffect,
  useState,
} from "react";

import {
  type LoginResponse,
  postApiAuthLogin,
  type PostApiAuthLoginError,
} from "@/api";

type AuthState = {
  token: string | null;
  expiresAt: number | null;
};

type AuthContextValue = {
  auth: AuthState;
  isAuthenticated: boolean;
  isLoading: boolean;
  login: (
    email: string,
    password: string,
  ) => Promise<
    | {
        data: LoginResponse;
        error: null;
      }
    | {
        data: null;
        error: PostApiAuthLoginError;
      }
  >;
  logout: () => void;
};

type AuthContextProviderProps = {
  children: ReactNode;
};

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within a AuthContextProvider");
  }
  return context;
}

function AuthContextProvider({ children }: AuthContextProviderProps) {
  const [auth, setAuth] = useState<AuthState>(() => {
    const storedToken = localStorage.getItem("token");
    const storedExpiresAt = localStorage.getItem("expiresAt");
    return {
      token: storedToken,
      expiresAt: storedExpiresAt ? Date.parse(storedExpiresAt) : null,
    };
  });
  const [isLoading, setIsLoading] = useState(false);
  const isAuthenticated =
    !!auth.token && !!auth.expiresAt && Date.now() < auth.expiresAt;

  const login = useCallback<AuthContextValue["login"]>(
    async (email, password) => {
      setIsLoading(true);
      const { data, error } = await postApiAuthLogin({
        body: {
          email,
          password,
        },
      });
      setIsLoading(false);

      if (data) {
        setAuth({
          token: data.token,
          expiresAt: Date.parse(data.expiresAt),
        });
        localStorage.setItem("token", data.token);
        localStorage.setItem("expiresAt", data.expiresAt);

        return { data, error: null };
      }

      return {
        data: null,
        error,
      };
    },
    [],
  );

  const logout = useCallback<AuthContextValue["logout"]>(() => {
    setAuth({
      token: null,
      expiresAt: null,
    });
    localStorage.removeItem("token");
    localStorage.removeItem("expiresAt");
  }, []);

  useEffect(() => {
    if (auth.expiresAt && Date.now() < auth.expiresAt) {
      const timeout = setTimeout(() => {
        logout();
      }, auth.expiresAt - Date.now());
      return () => clearTimeout(timeout);
    } else {
      logout();
    }
  }, [auth.expiresAt, logout]);

  return (
    <AuthContext.Provider
      value={{
        auth,
        isAuthenticated,
        isLoading,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export { useAuth, AuthContextProvider, type AuthContextValue };
