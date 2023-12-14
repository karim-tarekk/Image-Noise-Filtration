# Image-Filters
**The primary challenge in image enhancement is to suppress noise without causing significant blurring or loss of important image features. The project focuses on finding an optimal solution that balances noise reduction and preservation of image details.**

# Aim:
The project aims to enhance the quality of noisy images by applying two different filtering techniques: Alpha-Trim Filter and Adaptive Median Filter. Image noise is a common problem that arises during image acquisition, transmission, or processing, leading to degradation in image quality. The proposed filters are designed to effectively reduce different types of noise, such as salt-and-pepper noise and Gaussian noise, while preserving important image details.

## Alpha-Trim Filter:
The Alpha-Trim Filter is a nonlinear filter that removes outliers from the image data. It works by sorting the pixel values within a local neighborhood and discarding the extreme pixel values (highest and lowest) based on a specified parameter alpha. By omitting extreme values, the filter effectively reduces noise while maintaining the central pixel value within the neighborhood.

## Adaptive Median Filter:
The Adaptive Median Filter is another effective technique for removing impulse noise, such as salt-and-pepper noise. Unlike fixed-size filters, the adaptive median filter dynamically adjusts its window size based on the local characteristics of the image. It identifies the correct window size by analyzing the differences between the central pixel and its neighboring pixels. This adaptive approach helps in preserving image edges and details while effectively removing noise.

## Used Sorting Algorithms:
- Alpha-trim Filter:
  - Counting Sort
  - Heap Sort
- Adaptive Median Filter:
  - Counting Sort
  - Quick Sort
