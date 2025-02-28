// src/types/electron.d.ts
export {};

declare global {
  interface Window {
    electron: {
      minimize: () => void;
      maximize: () => void;
      close: () => void;
    };
  }
}
