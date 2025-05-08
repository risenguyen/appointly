import { Link } from "@tanstack/react-router";
import type { NavLink } from "@/lib/tanstack-router";

type DesktopNavProps = {
  navLinks: Array<NavLink>;
};

function DesktopNav({ navLinks }: DesktopNavProps) {
  return (
    <nav className="ml-4 hidden md:flex">
      <ul className="flex items-center gap-7">
        {navLinks.map((navLink) => (
          <li key={navLink.to} className="flex">
            <Link
              className="text-muted-foreground hover:text-foreground focus-visible:text-foreground data-[status=active]:text-foreground transition-colors"
              {...navLink}
            >
              {navLink.label}
            </Link>
          </li>
        ))}
      </ul>
    </nav>
  );
}

export default DesktopNav;
