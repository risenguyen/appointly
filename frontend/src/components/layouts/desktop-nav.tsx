import { Link } from "@tanstack/react-router";

function DesktopNav() {
  return (
    <nav className="ml-3 hidden md:flex">
      <ul className="flex items-center gap-7">
        <li className="flex">
          <Link
            activeOptions={{
              exact: true,
            }}
            className="text-muted-foreground hover:text-foreground data-[status=active]:text-foreground transition-colors"
            to="/app"
          >
            Dashboard
          </Link>
        </li>
        <li className="flex">
          <Link
            className="text-muted-foreground hover:text-foreground data-[status=active]:text-foreground transition-colors"
            to="/app/appointments"
          >
            Appointments
          </Link>
        </li>
        <li className="flex">
          <Link
            className="text-muted-foreground hover:text-foreground data-[status=active]:text-foreground transition-colors"
            to="/app/clients"
          >
            Clients
          </Link>
        </li>
        <li className="flex">
          <Link
            className="text-muted-foreground hover:text-foreground data-[status=active]:text-foreground transition-colors"
            to="/app/treatments"
          >
            Treatments
          </Link>
        </li>
        <li className="flex">
          <Link
            className="text-muted-foreground hover:text-foreground data-[status=active]:text-foreground transition-colors"
            to="/app/employees"
          >
            Employees
          </Link>
        </li>
      </ul>
    </nav>
  );
}

export default DesktopNav;
