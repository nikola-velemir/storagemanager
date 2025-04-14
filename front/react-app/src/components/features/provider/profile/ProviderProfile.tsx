import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ProviderProfileResponse } from "../../../../model/provider/ProviderProfileResponse";
import { ProviderService } from "../../../../services/ProviderService";
import ProviderContentTabs from "./ProviderContentTabs";

const ProviderProfile = () => {
  const { id } = useParams<{ id: string }>();

  const [provider, setProvider] = useState<ProviderProfileResponse | null>(
    null
  );
  useEffect(() => {
    if (!id || id.trim().length === 0) return;
    ProviderService.findProviderProfile(id).then((response) => {
      setProvider(response.data);
    });
  }, [id]);
  return (
    <div className="h-screen w-full p-8">
      <div className="w-full h-5/6 overflow-auto">
        <div className="px-8 pt-8 space-y-4 h-full">
          <div className="flex flex-row gap-12 w-full bg-gray-600 rounded-xl p-8">
            <div className="bg-slate-800 rounded-xl p-2">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                strokeWidth="1.5"
                stroke="currentColor"
                className="size-48"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  d="M17.982 18.725A7.488 7.488 0 0 0 12 15.75a7.488 7.488 0 0 0-5.982 2.975m11.963 0a9 9 0 1 0-11.963 0m11.963 0A8.966 8.966 0 0 1 12 21a8.966 8.966 0 0 1-5.982-2.275M15 9.75a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                />
              </svg>
            </div>
            <div className="flex flex-col justify-center w-full text-base text-gray-400">
              <div className="mt-2 flex flex-row items-center text-sm">
                Name:
                <span className="ms-3 text-white text-lg font-medium">
                  {provider?.name}
                </span>
              </div>
              <div className="mt-2 flex flex-row items-center text-sm">
                Address:
                <span className="ms-3 text-white text-lg font-medium">
                  {provider?.address}
                </span>
              </div>
              <div className="mt-2 flex flex-row items-center text-sm">
                Phone number:
                <span className="ms-3 text-white text-lg font-medium">
                  {provider?.phoneNumber}
                </span>
              </div>
            </div>
          </div>
          <ProviderContentTabs
            components={provider ? provider.components : []}
            invoices={provider ? provider.invoices : []}
          />
        </div>
      </div>
    </div>
  );
};

export default ProviderProfile;
