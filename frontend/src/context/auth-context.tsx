import { postApiAuthLogin } from "@/api";
import {
  createContext,
  ReactNode,
  useCallback,
  useContext,
  useState,
} from "react";

type AuthState = {
  token: string | null;
  expiresAt: number | null;
};

type AuthContextValue = {
  auth: AuthState;
  isAuthenticated: boolean;
  login: (email: string, password: string) => void;
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
      expiresAt: storedExpiresAt ? Number(storedExpiresAt) : null,
    };
  });

  const isAuthenticated =
    !!auth.token && !!auth.expiresAt && Date.now() < auth.expiresAt;

  const login = useCallback<AuthContextValue["login"]>(
    async (email, password) => {
      const {
        data: { token, expiresAt },
      } = await postApiAuthLogin({
        body: {
          email,
          password,
        },
        throwOnError: true,
      });
      setAuth({
        token,
        expiresAt: Number(expiresAt),
      });
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

  return (
    <AuthContext.Provider
      value={{
        auth,
        isAuthenticated,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export { useAuth, AuthContextProvider };
