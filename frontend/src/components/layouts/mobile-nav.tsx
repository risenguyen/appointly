import { Link } from "@tanstack/react-router";
import type { NavLink } from "@/lib/tanstack-router";
import { DrawerClose } from "@/components/ui/drawer";

type MobileNavProps = {
  navLinks: Array<NavLink>;
};

function MobileNav({ navLinks }: MobileNavProps) {
  return (
    <nav className="flex flex-col px-8">
      <ul className="flex flex-col gap-6 py-12">
        {navLinks.map((navLink) => (
          <li key={navLink.to} className="flex">
            <DrawerClose asChild>
              <Link
                className="data-[status=active]:text-foreground focus-visible:text-foreground text-muted-foreground hover:text-foreground text-2xl font-medium transition-colors"
                {...navLink}
              >
                {navLink.label}
              </Link>
            </DrawerClose>
          </li>
        ))}
      </ul>
    </nav>
  );
}

export default MobileNav;
