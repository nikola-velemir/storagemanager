import React, { useEffect, useState } from "react";
import { json } from "stream/consumers";
import * as XLSX from "xlsx";

interface ExcelViewerProps {
  fileSrc: Blob;
}

const ExcelViewer = ({ fileSrc }: ExcelViewerProps) => {
  const [data, setData] = useState<any[]>([]);
  const parseExcelFile = () => {
    const reader = new FileReader();
    reader.onload = (e: any) => {
      const arrayBuffer = e.target.result;
      const workBook = XLSX.read(arrayBuffer, { type: "array" });
      const sheetName = workBook.SheetNames[0];
      console.log(sheetName);
      const workSheet = workBook.Sheets[sheetName];
      console.log("aa", workSheet);
      const jsonData = XLSX.utils.sheet_to_json(workSheet, { header: 0 });
      console.log(jsonData);
      setData(jsonData);
    };
    reader.readAsArrayBuffer(fileSrc);
  };
  useEffect(() => {
    parseExcelFile();
  }, [fileSrc]);
  return (
    <>
      {data.length > 0 ? (
        <div className="relative overflow-y-scroll h-5/6 overflow-x-scroll">
          <table className="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
            <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
              <tr>
                {Object.keys(data[0]).map((key) => (
                  <th scope="col" className="px-6 py-3" key={key}>
                    {key}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody>
              {data.map((row, index) => (
                <tr
                  key={index}
                  className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200"
                >
                  {Object.values(row).map((value, i) => (
                    <td className="px-6 py-4" key={i}>
                      {value?.toString()}
                    </td>
                  ))}
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ) : (
        <p>Loading Excel data...</p>
      )}

      {/* <div>
        <h3>Excel Data</h3>
        {data.length > 0 ? (
          <table>
            <thead>
              <tr>
                {Object.keys(data[0]).map((key) => (
                  <th key={key}>{key}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {data.map((row, index) => (
                <tr key={index}>
                  {Object.values(row).map((value, i) => (
                    <td key={i}>{value?.toString()}</td>
                  ))}
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <p>Loading Excel data...</p>
        )}
      </div> */}
    </>
  );
};

export default ExcelViewer;
