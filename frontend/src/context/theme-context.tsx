import {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useState,
  type ReactNode,
} from "react";

const THEME_COLOR_LIGHT = "#ffffff";
const THEME_COLOR_DARK = "oklch(0.145 0 0)";

type Theme = "light" | "dark";

type ThemeContextValue = {
  theme: Theme;
  toggleTheme: () => void;
};

type ThemeContextProviderProps = {
  storageKey?: string;
  defaultTheme?: Theme;
  children: ReactNode;
};

const ThemeContext = createContext<ThemeContextValue | undefined>(undefined);

function useTheme() {
  const context = useContext(ThemeContext);
  if (!context) {
    throw new Error("useTheme must be used within a ThemeContextProvider");
  }
  return context;
}

function ThemeContextProvider({
  storageKey = "theme",
  defaultTheme = "light",
  children,
}: ThemeContextProviderProps) {
  const [theme, setTheme] = useState<Theme>(() => {
    const storedTheme = localStorage.getItem(storageKey);
    if (storedTheme === "light" || storedTheme === "dark") {
      return storedTheme;
    }
    return defaultTheme;
  });

  const toggleTheme = useCallback(
    () =>
      setTheme((previousTheme) =>
        previousTheme === "light" ? "dark" : "light",
      ),
    [setTheme],
  );

  useEffect(() => {
    localStorage.setItem(storageKey, theme);
    const root = document.documentElement;

    root.setAttribute("data-theme", theme);
    root.classList.add("disable-transitions");

    const timerId = setTimeout(() => {
      root.classList.remove("disable-transitions");
    }, 100);

    return () => clearTimeout(timerId);
  }, [theme, storageKey]);

  useEffect(() => {
    const metaThemeColor = document.querySelector("meta[name='theme-color']");

    if (metaThemeColor) {
      if (theme === "light") {
        metaThemeColor.setAttribute("content", THEME_COLOR_LIGHT);
      } else if (theme === "dark") {
        metaThemeColor.setAttribute("content", THEME_COLOR_DARK);
      }
    }
  }, [theme]);

  return (
    <ThemeContext.Provider
      value={{
        theme,
        toggleTheme,
      }}
    >
      {children}
    </ThemeContext.Provider>
  );
}

export { useTheme, ThemeContextProvider };
