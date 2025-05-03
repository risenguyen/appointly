import { Link } from "@tanstack/react-router";
import { TbMenu } from "react-icons/tb";
import {
  Drawer,
  DrawerTrigger,
  DrawerContent,
  DrawerClose,
  DrawerTitle,
  DrawerDescription,
} from "../ui/drawer";
import type { NavLink } from "@/lib/tanstack-router";

type MobileNavProps = {
  navLinks: Array<NavLink>;
};

function MobileNav({ navLinks }: MobileNavProps) {
  return (
    <Drawer autoFocus>
      <DrawerTrigger asChild>
        <button
          aria-label="Open Navigation"
          className="flex cursor-pointer items-center md:hidden"
          type="button"
        >
          <TbMenu className="leading-0" size="20px" />
        </button>
      </DrawerTrigger>
      <DrawerContent className="">
        <DrawerTitle className="sr-only">Mobile Navigation Menu</DrawerTitle>
        <DrawerDescription className="sr-only">
          Contains links to navigate the application sections.
        </DrawerDescription>
        <nav className="flex flex-col px-8">
          <ul className="flex flex-col gap-6 py-12">
            {navLinks.map((navLink) => (
              <li key={navLink.to} className="flex">
                <DrawerClose asChild>
                  <Link
                    className="data-[status=active]:text-foreground text-muted-foreground hover:text-foreground text-2xl font-medium transition-colors"
                    {...navLink}
                  >
                    {navLink.label}
                  </Link>
                </DrawerClose>
              </li>
            ))}
          </ul>
        </nav>
      </DrawerContent>
    </Drawer>
  );
}

export default MobileNav;
