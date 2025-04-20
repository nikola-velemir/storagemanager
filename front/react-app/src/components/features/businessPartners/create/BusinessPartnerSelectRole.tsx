import React, { useEffect, useState } from "react";
import { BusinessPartnerRoles } from "../../../../model/businessPartner/BusinessPartnerRoles";

interface BusinessPartnerSelectRoleProps {
  role: BusinessPartnerRoles;
  emitRole: (role: BusinessPartnerRoles) => void;
}

const BusinessPartnerSelectRole = ({
  role,
  emitRole,
}: BusinessPartnerSelectRoleProps) => {
  const roles = Object.values(BusinessPartnerRoles);
  const [selectedRole, setSelectedRole] = useState(role);
  useEffect(() => setSelectedRole(role), [role]);
  const handleRoleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const role = e.target.value;
    const foundRole = roles.find((r) => r === role);
    if (!foundRole) return;
    setSelectedRole(foundRole);
    emitRole(foundRole);
  };
  return (
    <form className="max-w-sm mx-auto">
      <label
        htmlFor="countries"
        className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
      >
        Select Role{" "}
      </label>
      <select
        value={selectedRole}
        id="countries"
        onChange={handleRoleChange}
        className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
      >
        {roles.map((role: BusinessPartnerRoles) => {
          return (
            <option value={role} key={role}>
              {role.toString()}
            </option>
          );
        })}
      </select>
    </form>
  );
};

export default BusinessPartnerSelectRole;
