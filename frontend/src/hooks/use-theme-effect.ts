import { useEffect } from "react";

const THEME_COLOR_LIGHT = "#ffffff";
const THEME_COLOR_DARK = "hsl(0 0% 14.5%)";

export const useThemeEffect = (theme: string) => {
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
};
